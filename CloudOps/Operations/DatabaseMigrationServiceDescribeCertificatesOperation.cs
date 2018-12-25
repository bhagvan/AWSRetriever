using Amazon;
using Amazon.DatabaseMigrationService;
using Amazon.DatabaseMigrationService.Model;
using Amazon.Runtime;

namespace CloudOps.Operations
{
    public class DatabaseMigrationServiceDescribeCertificatesOperation : Operation
    {
        public override string Name => "DescribeCertificates";

        public override string Description => "Provides a description of the certificate.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "DatabaseMigrationService";

        public override string ServiceID => "Database Migration Service";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonDatabaseMigrationServiceClient client = new AmazonDatabaseMigrationServiceClient(creds, region);
            DescribeCertificatesResponse resp = new DescribeCertificatesResponse();
            do
            {
                DescribeCertificatesMessage req = new DescribeCertificatesMessage
                {
                    Marker = resp.Marker
                    ,
                    MaxRecords = maxItems
                                        
                };

                resp = client.DescribeCertificates(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.&lt;nil&gt;)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.Marker));
        }
    }
}