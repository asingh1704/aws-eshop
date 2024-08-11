using Amazon.CDK;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Route53;
using Amazon.CDK.AWS.Route53.Targets;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.S3.Deployment;
using Constructs;
using System.Net.Sockets;

namespace AwsEshopApp
{
    public class AwsEshopAppStack : Stack
    {
        public AwsEshopAppStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {

            //setting up S3
            var siteBucket = new Bucket(this, "aws-eshop-bucket", new BucketProps
            {
                BucketName = "eshoponcloud.xyz",
                WebsiteIndexDocument = "index.html",
                PublicReadAccess = true,
                RemovalPolicy = RemovalPolicy.DESTROY,
                AutoDeleteObjects = true,
                BlockPublicAccess = new BlockPublicAccess(new BlockPublicAccessOptions())
                {
                    BlockPublicAcls = false,
                    BlockPublicPolicy = false,
                    IgnorePublicAcls = false,
                    RestrictPublicBuckets = false,
                }
            });

            siteBucket.AddToResourcePolicy(new PolicyStatement(new PolicyStatementProps
            {
                Effect = Effect.ALLOW,
                Principals = new[] { new AnyPrincipal() },
                Actions = new[] { "s3:GetObject" },
                Resources = new[] { siteBucket.ArnForObjects("*") }
            }));

            // Deploy the React app to the S3 bucket
            new BucketDeployment(this, "DeployReactApp", new BucketDeploymentProps
            {
                Sources = new[] { Source.Asset("../../../clients/aws-eshop-app/build") }, // Update this path
                DestinationBucket = siteBucket
            });

            //setting up route53

            var hostedZone = HostedZone.FromLookup(this, "aws-eshop-hostedzone", new HostedZoneProviderProps
            {
                DomainName = "eshoponcloud.xyz"
            });

            new ARecord(this, "aws-eshop-aliasrecord", new ARecordProps
            {
                Zone = hostedZone,
                Target = RecordTarget.FromAlias(new BucketWebsiteTarget(siteBucket)),
                RecordName = "eshoponcloud.xyz",
            });
        }
    }
}
