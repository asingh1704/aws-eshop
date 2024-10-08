﻿using Amazon.CDK;

namespace AwsEshopApp
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            //new AwsEshopAppStack(app, "AwsEshopAppStack", new StackProps
            //{
            //    Env = new Amazon.CDK.Environment
            //    {
            //        Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
            //        Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION"),
            //    }
            //});
            new AwsEshopSQSStack(app, "AwsEshopSQSStack", new StackProps
            {
                Env = new Amazon.CDK.Environment
                {
                    Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
                    Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION"),
                }
            });
            
            new CognitoStack(app, "AwsEshopCognitoStack", new StackProps
            {
                Env = new Amazon.CDK.Environment
                {
                    Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
                    Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION"),
                }
            });
            app.Synth();
        }
    }
}
