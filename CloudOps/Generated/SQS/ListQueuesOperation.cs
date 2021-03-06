using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Amazon.Runtime;

namespace CloudOps.SQS
{
    public class ListQueuesOperation : Operation
    {
        public override string Name => "ListQueues";

        public override string Description => "Returns a list of your queues. The maximum number of queues that can be returned is 1,000. If you specify a value for the optional QueueNamePrefix parameter, only queues with a name that begins with the specified value are returned.  Cross-account permissions don&#39;t apply to this action. For more information, see see Grant Cross-Account Permissions to a Role and a User Name in the Amazon Simple Queue Service Developer Guide. ";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "SQS";

        public override string ServiceID => "SQS";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonSQSConfig config = new AmazonSQSConfig();
            config.RegionEndpoint = region;
            ConfigureClient(config);            
            AmazonSQSClient client = new AmazonSQSClient(creds, config);
            
            ListQueuesResponse resp = new ListQueuesResponse();
            ListQueuesRequest req = new ListQueuesRequest
            {                    
                                    
            };
            resp = client.ListQueues(req);
            CheckError(resp.HttpStatusCode, "200");                
            
            foreach (var obj in resp.QueueUrls)
            {
                AddObject(obj);
            }
            
        }
    }
}