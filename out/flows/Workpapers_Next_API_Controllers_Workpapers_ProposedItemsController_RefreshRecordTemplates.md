[web] PUT /api/proposed-items/record-templates/refresh  (Workpapers.Next.API.Controllers.Workpapers.ProposedItemsController.RefreshRecordTemplates)  [L117–L139] status=200 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.WriteQuery [L120]
  └─ write Binder [L120]
    └─ reads_from Binders
  └─ sends_request CreateUpdateProposedItemsCommand -> CreateUpdateProposedItemsCommandHandler [L136]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.ProposedItems.CreateUpdateProposedItemsCommandHandler.Handle [L36–L184]
      └─ uses_service IControlledRepository<ProposedItem> (Scoped (inferred))
        └─ method WriteQuery [L63]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.ProposedItemRepository.WriteQuery
  └─ sends_request GetProposedRecordTemplatesQuery -> GetProposedRecordTemplatesQueryHandler [L135]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.GetProposedRecordTemplatesQueryHandler.Handle [L45–L242]
      └─ calls SourceAccountRepository.ReadQuery [L106]
      └─ calls ClientRepository.ReadQuery [L89]
      └─ maps_to WorkpaperRecordTemplateUltraLightDto [L167]
        └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecordTemplate->WorkpaperRecordTemplateUltraLightDto) [L216]
      └─ maps_to SourceAccountUltraLightDto [L106]
        └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountUltraLightDto) [L615]
      └─ maps_to BinderTemplateDto [L95]
        └─ automapper.registration WorkpapersMappingProfile (BinderTemplate->BinderTemplateDto) [L726]
      └─ uses_client ClientRepository [L89]
      └─ uses_service IControlledRepository<RollOverRecord> (Scoped (inferred))
        └─ method ReadQuery [L156]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.RollOverRecordRepository.ReadQuery
      └─ uses_service UnitOfWork
        └─ method LocalTable [L154]
          └─ implementation UnitOfWork.LocalTable
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate> (Scoped (inferred))
        └─ method ReadQuery [L129]
          └─ implementation Workpapers.Next.Data.Repository.Templates.WorkpaperRecordTemplateRepository.ReadQuery
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L98]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<BinderTemplate> (Scoped (inferred))
        └─ method ReadQuery [L95]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.BinderTemplateRepository.ReadQuery
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L83]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
  └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L128]
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
                      └─ implementation Workpapers.Next.DTOs.FirmLightDto.AssignActiveTenant [L8-L17]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L116]
      └─ uses_service UserService
        └─ method GetUserId [L91]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
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
    └─ entities 1 (writes=1, reads=0)
      └─ Binder writes=1 reads=0
    └─ clients 1
      └─ ClientRepository
    └─ requests 3
      └─ CanIAccessBinderQuery
      └─ CreateUpdateProposedItemsCommand
      └─ GetProposedRecordTemplatesQuery
    └─ handlers 3
      └─ CanIAccessBinderQueryHandler
      └─ CreateUpdateProposedItemsCommandHandler
      └─ GetProposedRecordTemplatesQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 3
      └─ BinderTemplateDto
      └─ SourceAccountUltraLightDto
      └─ WorkpaperRecordTemplateUltraLightDto

