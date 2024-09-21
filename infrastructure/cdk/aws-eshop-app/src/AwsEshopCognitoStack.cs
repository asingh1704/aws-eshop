using Amazon.CDK;
using Amazon.CDK.AWS.Cognito;
using Constructs;

namespace AwsEshopApp
{
    public class CognitoStack : Stack
    {
        internal CognitoStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // Create a new Cognito User Pool
            var userPool = new UserPool(this, "EshopUserPool", new UserPoolProps
            {
                UserPoolName = "aws-eshop.users",
                SignInAliases = new SignInAliases
                {
                    Username = true // Allow sign-in by username
                },
                PasswordPolicy = new PasswordPolicy
                {
                    MinLength = 8, // Custom password policy, 8 characters min, no other restrictions
                    RequireLowercase = false,
                    RequireUppercase = false,
                    RequireDigits = false,
                    RequireSymbols = false
                },
                SelfSignUpEnabled = true,
                AutoVerify = new AutoVerifiedAttrs
                {
                    Email = true // Automatically verify email
                },
                StandardAttributes = new StandardAttributes
                {
                    Email = new StandardAttribute
                    {
                        Required = true,
                        Mutable = true
                    },
                    Fullname = new StandardAttribute
                    {
                        Required = true,
                        Mutable = true
                    }
                },
                Mfa = Mfa.OPTIONAL, // No MFA required
                
            });

            // Create the app client for public use (no secret)
            var appClient = userPool.AddClient("AppClient", new UserPoolClientOptions
            {
                UserPoolClientName = "aws-eshop.app",
                GenerateSecret = false, // Public client, no secret
                OAuth = new OAuthSettings
                {
                    Flows = new OAuthFlows
                    {
                        AuthorizationCodeGrant = true
                    },
                    CallbackUrls = new[] { "http://localhost:31090" }, // Redirect URL for your React app
                    Scopes = new[] { OAuthScope.OPENID, OAuthScope.EMAIL, OAuthScope.PROFILE }
                },
                AccessTokenValidity = Duration.Minutes(60),
                IdTokenValidity = Duration.Minutes(60),
                RefreshTokenValidity = Duration.Days(30)
            });

            // Enable Cognito Hosted UI
            new CfnUserPoolDomain(this, "CognitoDomain", new CfnUserPoolDomainProps
            {
                Domain = "eshop", // This will create the domain aws-eshop.auth.<region>.amazoncognito.com
                UserPoolId = userPool.UserPoolId
            });
        }
    }
}



