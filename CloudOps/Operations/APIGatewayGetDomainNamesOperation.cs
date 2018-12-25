using Amazon;
using Amazon.APIGateway;
using Amazon.APIGateway.Model;
using Amazon.Runtime;

namespace CloudOps.Operations
{
    public class APIGatewayGetDomainNamesOperation : Operation
    {
        public override string Name => "GetDomainNames";

        public override string Description => "Represents a collection of DomainName resources.";
 
        public override string RequestURI => "/domainnames";

        public override string Method => "GET";

        public override string ServiceName => "APIGateway";

        public override string ServiceID => "API Gateway";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonAPIGatewayClient client = new AmazonAPIGatewayClient(creds, region);
            DomainNames resp = new DomainNames();
            do
            {
                GetDomainNamesRequest req = new GetDomainNamesRequest
                {
                    position = resp.position
                    ,
                    limit = maxItems
                                        
                };

                resp = client.GetDomainNames(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.items)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.position));
        }
    }
}