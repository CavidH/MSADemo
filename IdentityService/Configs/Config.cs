using IdentityServer4.Models;

namespace IdentityService.Configs
{
    public class Config
    {
        #region Scopes

        //Api lərdəki icazələr 
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("ServiceA.Write", "Write Scope :) "),
                new ApiScope("ServiceA.Read", "Read Scope :)"),
                new ApiScope("ServiceB.Write", "Write Scope :)"),
                new ApiScope("ServiceB.Read", "Read Scope :)"),
                new ApiScope("ServiceB.Admin", "Admin Scope :)")
            };
        }

        #endregion

        #region ApiResources

        //api lər  (services)

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("ServiceA") { Scopes = { "ServiceA.Read", "ServiceA.Write","ServiceB.Admin" } },
                new ApiResource("ServiceB") { Scopes = { "ServiceB.Read", "ServiceB.Write" } }
            };
        }

        #endregion


        #region Clients

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client
                {
                    ClientId = "Client1",
                    ClientName = "Client1",
                    ClientSecrets = { new Secret("Client1".Sha256()) },
                    AllowedGrantTypes = { GrantType.ClientCredentials },
                    AllowedScopes = { "ServiceB.Read"/*,"ServiceA.Read" */}
                },
                new Client
                {
                    ClientId = "Client2",
                    ClientName = "Client2",
                    ClientSecrets = { new Secret("Client2".Sha256()) },
                    AllowedGrantTypes = { GrantType.ClientCredentials },
                    AllowedScopes = { "ServiceB.Write","ServiceA.Write"  }
                }
            };
        }

        #endregion
    }
}
