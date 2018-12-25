using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Runtime;

namespace CloudOps.Operations
{
    public class EC2DescribeVpnGatewaysOperation : Operation
    {
        public override string Name => "DescribeVpnGateways";

        public override string Description => "Describes one or more of your virtual private gateways. For more information about virtual private gateways, see AWS Managed VPN Connections in the Amazon Virtual Private Cloud User Guide.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "EC2";

        public override string ServiceID => "EC2";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonEC2Client client = new AmazonEC2Client(creds, region);
            DescribeVpnGatewaysResult resp = new DescribeVpnGatewaysResult();
            DescribeVpnGatewaysRequest req = new DescribeVpnGatewaysRequest
            {                    
                                    
            };
            resp = client.DescribeVpnGateways(req);
            CheckError(resp.HttpStatusCode, "200");                
            
            foreach (var obj in resp.VpnGateways)
            {
                AddObject(obj);
            }
            
        }
    }
}