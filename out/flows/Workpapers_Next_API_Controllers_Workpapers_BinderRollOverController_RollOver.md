[web] PUT /api/binder-roll-over/  (Workpapers.Next.API.Controllers.Workpapers.BinderRollOverController.RollOver)  [L76–L89] status=200 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.WriteQuery [L79]
  └─ write Binder [L79]
    └─ reads_from Binders
  └─ sends_request RollOverBinderCommand -> RollOverBinderCommandHandler [L86]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.Binders.RollOverBinderCommandHandler.Handle [L57–L911]
      └─ maps_to RollOverRecordModel [L368]
      └─ maps_to RollOverHyperlinkModel [L353]
      └─ maps_to RollOverMatterModel [L349]
      └─ maps_to BinderSectionCreateModel [L251]
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate> (Scoped (inferred))
        └─ method ReadQuery [L499]
          └─ implementation Workpapers.Next.Data.Repository.Templates.WorkpaperRecordTemplateRepository.ReadQuery
      └─ uses_service UnitOfWork
        └─ method Table [L387]
          └─ implementation UnitOfWork.Table
      └─ uses_service IControlledRepository<ArchivedWorkpaperRecordTemplateMapping> (Scoped (inferred))
        └─ method ReadQuery [L320]
          └─ implementation Workpapers.Next.Data.Repository.Templates.ArchivedWorkpaperRecordTemplateMappingRepository.ReadQuery
      └─ uses_service IControlledRepository<WorkpaperRecord> (Scoped (inferred))
        └─ method ReadQuery [L298]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorkpaperRecordRepository.ReadQuery
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L251]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
      └─ uses_service IControlledRepository<RollOverHyperlink> (Scoped (inferred))
        └─ method WriteQuery [L237]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.RollOverHyperlinkRepository.WriteQuery
      └─ uses_service IControlledRepository<RollOverMatter> (Scoped (inferred))
        └─ method WriteQuery [L233]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.RollOverMatterRepository.WriteQuery
      └─ uses_service IControlledRepository<RollOverWorksheet> (Scoped (inferred))
        └─ method WriteQuery [L229]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.RollOverWorksheetRepository.WriteQuery
      └─ uses_service IControlledRepository<RollOverRecord> (Scoped (inferred))
        └─ method WriteQuery [L224]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.RollOverRecordRepository.WriteQuery
      └─ uses_service UserService
        └─ method GetUserId [L179]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
      └─ uses_service IControlledRepository<MatterStatus> (AddScoped)
        └─ method ReadQuery [L165]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Matter> (Scoped (inferred))
        └─ method ReadQuery [L157]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.MatterRepository.ReadQuery
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L146]
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
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L112]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L85]
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
          └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant (see previous expansion)
      └─ uses_service UserService
        └─ method GetUserId [L91]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId (see previous expansion)
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
    └─ requests 2
      └─ CanIAccessBinderQuery
      └─ RollOverBinderCommand
    └─ handlers 2
      └─ CanIAccessBinderQueryHandler
      └─ RollOverBinderCommandHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 4
      └─ BinderSectionCreateModel
      └─ RollOverHyperlinkModel
      └─ RollOverMatterModel
      └─ RollOverRecordModel

