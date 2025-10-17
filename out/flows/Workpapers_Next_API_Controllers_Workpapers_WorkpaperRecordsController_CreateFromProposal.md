[web] POST /api/workpaper-records/from-proposal  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.CreateFromProposal)  [L182–L197] status=201 [auth=AuthorizationPolicies.User]
  └─ calls BinderRepository.ReadQuery [L185]
  └─ query Binder [L185]
    └─ reads_from Binders
  └─ sends_request CreateWorkpaperRecordCommand -> CreateWorkpaperRecordCommandHandler [L194]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.WorkpaperRecords.CreateWorkpaperRecordCommandHandler.Handle [L36–L149]
      └─ uses_service IControlledRepository<LoanMatrix> (Scoped (inferred))
        └─ method WriteQuery [L104]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.LoanMatrixRepository.WriteQuery
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L99]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L70]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<WorkpaperRecord> (Scoped (inferred))
        └─ method WriteQuery [L60]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorkpaperRecordRepository.WriteQuery
  └─ sends_request CreateWorkpaperRecordModelFromProposalCommand -> CreateWorkpaperRecordModelFromProposalCommandHandler [L193]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.WorkpaperRecords.CreateWorkpaperRecordModelFromProposalCommandHandler.Handle [L34–L335]
      └─ calls SourceAccountRepository.ReadQuery [L136]
      └─ uses_service IControlledRepository<RollOverRecord> (Scoped (inferred))
        └─ method ReadQuery [L270]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.RollOverRecordRepository.ReadQuery
      └─ uses_service UnitOfWork
        └─ method LocalTable [L268]
          └─ implementation UnitOfWork.LocalTable
      └─ uses_service IControlledRepository<Worksheet> (Scoped (inferred))
        └─ method ReadQuery [L198]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorksheetRepository.ReadQuery
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L183]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<WorkpaperRecordTemplate> (Scoped (inferred))
        └─ method ReadQuery [L110]
          └─ implementation Workpapers.Next.Data.Repository.Templates.WorkpaperRecordTemplateRepository.ReadQuery
      └─ uses_service IControlledRepository<BinderRecordTemplate> (Scoped (inferred))
        └─ method ReadQuery [L85]
          └─ implementation Workpapers.Next.Data.Repository.Templates.BinderRecordTemplateRepository.ReadQuery
      └─ uses_service IControlledRepository<RecordStatus> (AddScoped)
        └─ method WriteQuery [L70]
          └─ ... (no implementation details available)
  └─ sends_request CanIAccessBinderQuery -> CanIAccessBinderQueryHandler [L191]
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
    └─ entities 1 (writes=0, reads=1)
      └─ Binder writes=0 reads=1
    └─ requests 3
      └─ CanIAccessBinderQuery
      └─ CreateWorkpaperRecordCommand
      └─ CreateWorkpaperRecordModelFromProposalCommand
    └─ handlers 3
      └─ CanIAccessBinderQueryHandler
      └─ CreateWorkpaperRecordCommandHandler
      └─ CreateWorkpaperRecordModelFromProposalCommandHandler
    └─ caches 1
      └─ IMemoryCache

