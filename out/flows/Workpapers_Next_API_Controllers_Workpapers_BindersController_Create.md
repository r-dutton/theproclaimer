[web] POST /api/binders/  (Workpapers.Next.API.Controllers.Workpapers.BindersController.Create)  [L189–L242] status=201 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.Add [L221]
  └─ insert Binder [L221]
    └─ reads_from Binders
  └─ uses_service UserService
    └─ method GetUser [L229]
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
  └─ sends_request CreateBinderDocumentCommand -> CreateBinderDocumentCommandHandler [L229]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Binders.CreateBinderDocumentCommandHandler.Handle [L32–L66]
      └─ uses_service DataverseService
        └─ method GetStandardQueryParams [L60]
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
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L51]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ sends_request CanBinderBeCreatedQuery -> CanBinderBeCreatedQueryHandler [L215]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.CanBinderBeCreatedQueryHandler.Handle [L36–L151]
      └─ maps_to BinderTemplateDto [L77]
        └─ automapper.registration WorkpapersMappingProfile (BinderTemplate->BinderTemplateDto) [L726]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate> (Scoped (inferred))
        └─ method ReadQuery [L117]
          └─ implementation Workpapers.Next.Data.Repository.Templates.WorkpaperRecordTemplateRepository.ReadQuery
      └─ uses_service IControlledRepository<BinderTemplate> (Scoped (inferred))
        └─ method ReadQuery [L77]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.BinderTemplateRepository.ReadQuery
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L63]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Binder writes=1 reads=0
    └─ services 1
      └─ UserService
    └─ requests 2
      └─ CanBinderBeCreatedQuery
      └─ CreateBinderDocumentCommand
    └─ handlers 2
      └─ CanBinderBeCreatedQueryHandler
      └─ CreateBinderDocumentCommandHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ BinderTemplateDto

