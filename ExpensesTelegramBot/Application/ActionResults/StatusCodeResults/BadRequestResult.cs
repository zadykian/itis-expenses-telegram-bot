﻿namespace Application
{
    public class BadRequestResult : StatusCodeResult
    {
        public override int StatusCode => 400;
    }
}