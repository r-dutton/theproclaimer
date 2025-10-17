[web] PUT /api/binders/{binderId:guid}/hyperlinks/bulk-update  (Workpapers.Next.API.Controllers.Workpapers.BinderHyperlinksController.BulkUpdateHyperlinks)  [L55–L63] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request BulkUpdateBinderHyperlinksCommand -> BulkUpdateBinderHyperlinksCommandHandler [L60]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.WorkpaperRecords.BulkUpdateBinderHyperlinksCommandHandler.Handle [L36–L267]
      └─ maps_to RecordStatusDto [L230]
        └─ automapper.registration ExternalApiMappingProfile (RecordStatus->RecordStatusDto) [L158]
        └─ automapper.registration WorkpapersMappingProfile (RecordStatus->RecordStatusDto) [L400]
      └─ uses_service IControlledRepository<RecordStatus> (AddScoped)
        └─ method ReadQuery [L230]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<WorkpaperRecord> (Scoped (inferred))
        └─ method WriteQuery [L172]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorkpaperRecordRepository.WriteQuery
      └─ uses_service IControlledRepository<Hyperlink> (AddScoped)
        └─ method WriteQuery [L79]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.HyperlinkRepository.WriteQuery
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L70]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L64]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ BulkUpdateBinderHyperlinksCommand
    └─ handlers 1
      └─ BulkUpdateBinderHyperlinksCommandHandler
    └─ mappings 1
      └─ RecordStatusDto

