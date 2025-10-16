[web] GET /api/tenant/databases/regions  (Dataverse.Api.Controllers.Tenants.DatabaseController.GetRegions)  [L54–L68] status=200 [auth=master]
  └─ calls TenantRepository.ReadTable [L57]
  └─ query Tenant [L57]
    └─ reads_from Tenants
  └─ uses_service UserService
    └─ method GetIdentityId [L61]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetIdentityId [L15-L185]
        └─ uses_service RequestInfoService
          └─ method GetIdentityId [L160]
            └─ implementation Dataverse.Services.Features.RequestInfoService.GetIdentityId [L11-L92]
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
  └─ uses_service TenantRepository
    └─ method ReadTable [L57]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Tenant writes=0 reads=1
    └─ services 2
      └─ TenantRepository
      └─ UserService

