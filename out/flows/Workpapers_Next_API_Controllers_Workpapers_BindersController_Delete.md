[web] DELETE /api/binders/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BindersController.Delete)  [L475–L511] status=204 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository (methods: Remove,WriteQuery) [L502]
  └─ delete Binder [L502]
    └─ reads_from Binders
  └─ write Binder [L481]
    └─ reads_from Binders
  └─ uses_service UserService
    └─ method GetUser [L491]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
        └─ uses_service RequestProcessor
          └─ method GetUserByDataverseId [L287]
            └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
            └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
            └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
            └─ +1 additional_requests elided
        └─ uses_service bool?
          └─ method IsSuperUser [L174]
            └─ ... (no implementation details available)
        └─ uses_service Firm>
          └─ method GetFirmId [L139]
            └─ ... (no implementation details available)
        └─ uses_service User>
          └─ method GetUserIdFromToken [L106]
            └─ ... (no implementation details available)
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ uses_service DataverseService
    └─ method GetStandardQueryParams [L490]
      └─ implementation Workpapers.Next.Dataverse.Service.DataverseService.GetStandardQueryParams [L17-L127]
        └─ uses_service TenantIdentificationService
          └─ method GetStandardQueryParams [L70]
            └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetStandardQueryParams [L15-L131]
              └─ uses_service RequestProcessor
                └─ method GetCatalogByFirmId [L104]
                  └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                  └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                  └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                  └─ +1 additional_requests elided
              └─ uses_service FirmLightDto
                └─ method AssignActiveTenant [L77]
                  └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant [L8-L17]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L116]
  └─ sends_request CreateDataverseAuditHistoryCommand -> CreateDataverseAuditHistoryQueryHandler [L504]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.Firm.CreateDataverseAuditHistoryQueryHandler.Handle [L37–L84]
      └─ uses_service DataverseService
        └─ method Post [L75]
          └─ implementation Cirrus.Central.Features.MachineToMachine.DataverseService.Post [L14-L66]
            └─ uses_service TenantService
              └─ method GetStandardQueryParams [L62]
                └─ implementation Cirrus.Central.Tenants.TenantService.GetStandardQueryParams [L26-L139]
                  └─ uses_service IRequestProcessor (InstancePerDependency)
                    └─ method GetCatalogByDataverseId [L111]
                      └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByDataverseId [L7-L35]
                  └─ uses_service Tenant
                    └─ method AssignCurrentTenantId [L80]
                      └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId [L20-L187]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L104]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUser [L53]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser (see previous expansion)
      └─ uses_service ITenantService (AddScoped)
        └─ method GetCurrentTenant [L52]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenant [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                  └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L483]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanIAccessBinderQueryHandler.Handle [L60–L126]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L117]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L101]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L92]
          └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
                  └─ uses_service RequestProcessor
                    └─ method GetCatalogByFirmId [L104]
                      └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                      └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                      └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByFirmId
                      └─ +1 additional_requests elided
                  └─ uses_service FirmLightDto
                    └─ method AssignActiveTenant [L77]
                      └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant (see previous expansion)
      └─ uses_service UserService
        └─ method GetUserId [L91]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId (see previous expansion)
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L89]
          └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
            └─ uses_service RequestInfo
              └─ method IsValidServiceAccountRequest [L71]
                └─ implementation Cirrus.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
                  └─ uses_service RequestInfo
                    └─ method IsValidServiceAccountRequest [L82]
                      └─ ... (service recursion detected)
                  └─ logs ILogger<IRequestInfoService> [Warning] [L89]
                └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
            └─ logs ILogger<IRequestInfoService> [Warning] [L81]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L121]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L109]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L92]
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ Binder writes=2 reads=0
    └─ services 2
      └─ DataverseService
      └─ UserService
    └─ requests 2
      └─ CanIAccessBinderQuery
      └─ CreateDataverseAuditHistoryCommand
    └─ handlers 2
      └─ CanIAccessBinderQueryHandler
      └─ CreateDataverseAuditHistoryQueryHandler
    └─ caches 1
      └─ IMemoryCache

