[web] POST /core/v1/offices/  (Dataverse.Api.External.Controllers.v1.Core.OfficesController.Create)  [L125–L133] status=201 [auth=Authentication.CoreWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method PostSerialisedModel [L131]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.PostSerialisedModel [L12-L74]
        └─ uses_client DataverseClient [L28]
        └─ uses_service RequestInfoService
          └─ method AppendStandardParams [L62]
            └─ implementation Dataverse.Services.Features.RequestInfoService.AppendStandardParams [L11-L92]
              └─ uses_service RequestInfo
                └─ method IsValidServiceAccountRequest [L82]
                  └─ implementation Cirrus.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                  └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
                    └─ uses_service RequestInfo
                      └─ method IsValidServiceAccountRequest [L82]
                        └─ ... (service recursion detected)
                    └─ logs ILogger<IRequestInfoService> [Warning] [L89]
                  └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                    └─ uses_service RequestInfo
                      └─ method IsValidServiceAccountRequest [L71]
                        └─ ... (service recursion detected)
                    └─ logs ILogger<IRequestInfoService> [Warning] [L81]
              └─ logs ILogger<IRequestInfoService> [Warning] [L89]
        └─ uses_service TenantService
          └─ method AppendStandardParams [L59]
            └─ implementation Dataverse.Services.Features.Tenants.TenantService.AppendStandardParams [L6-L27]
              └─ uses_service TenantIdentificationService
                └─ method GetCurrentTenant [L20]
                  └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                    └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                    └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                    └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
        └─ uses_service DataverseClient
          └─ method Post [L28]
            └─ ... (no implementation details available)
  └─ impact_summary
    └─ clients 1
      └─ DataverseClient
    └─ services 1
      └─ IDataverseProxyService
    └─ caches 1
      └─ IMemoryCache

