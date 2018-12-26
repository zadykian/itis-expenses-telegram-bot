using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Infrastructure;
using Core;
using MvcWebLibrary;
using System.Linq;

namespace Application
{
    public class UserController : DbAccessController
    {
        public UserController(ApplicationContext dbContext)
            : base(dbContext)
        {
        }

        [HttpGet]
        public IActionResult GetStatistics(StatisticResult statisticResult)
        {
            var channelWithGivenId = dbContext.Channels.Find(statisticResult.ChannelId);
            if (channelWithGivenId == null)
            {
                return NotFound();
            }

            var requiredCategories = statisticResult.CategoriesToAmounts
                .Select(pair => pair.Key.ToLowerInvariant());

            var userChannels = dbContext.Channels
                .Where(channel => channel.User.Equals(channelWithGivenId.User));

            var singleExpenses = dbContext.SingleExpenses
                .Where(expense => userChannels.Contains(expense.Channel) 
                       && expense.CreationDateTime >= statisticResult.StartDateTime
                       && expense.CreationDateTime <= statisticResult.EndDateTime)
                .GroupBy(expense => expense.Category)
                .Where(group => requiredCategories.Contains(group.Key))
                .ToDictionary(group => group.Key, group => group.Sum(expense => expense.Amount));

            statisticResult.CategoriesToAmounts = singleExpenses;
            return Json(statisticResult);
        }

        [HttpGet]
        public IActionResult GetRegularExpensesCategories(Channel channel)
        {
            var user = dbContext.Channels.Find(channel.Id)?.User;
            if (user == null)
            {
                return NotFound();
            }
            var regularCategories = dbContext.RegularExpensesCategories
                .Where(expensesCat => expensesCat.User.Equals(user))
                .ToList();
            return Json(regularCategories);
        }

        [HttpPost]
        public IActionResult AddSingleExpense(SingleExpense singleExpense)
        {
            dbContext.SingleExpenses.Add(singleExpense);
            return Ok();
        }

        [HttpPost]
        public IActionResult AddExpensesCategory(Channel channel, RegularExpensesCategory expensesCategory)
        {
            var user = dbContext.Channels.Find(channel.Id)?.User;
            if (user == null)
            {
                return BadRequest();
            }
            expensesCategory.SetUser(user);

            if (dbContext.RegularExpensesCategories.Find(expensesCategory) == null)
            {
                dbContext.RegularExpensesCategories.Add(expensesCategory);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult RemoveExpensesCategory(Channel channel, RegularExpensesCategory expensesCategory)
        {
            var user = dbContext.Channels.Find(channel.Id)?.User;
            if (user == null)
            {
                return BadRequest();
            }
            var expensesCategoryToRemove = dbContext.RegularExpensesCategories
                .FirstOrDefault(cat => cat.User.Equals(user)
                    && cat.Category.Equals(expensesCategory.Category, StringComparison.InvariantCultureIgnoreCase));
            if (expensesCategoryToRemove != null)
            {
                dbContext.RegularExpensesCategories.Remove(expensesCategoryToRemove);
            }
            return Ok();
        }
    }
}