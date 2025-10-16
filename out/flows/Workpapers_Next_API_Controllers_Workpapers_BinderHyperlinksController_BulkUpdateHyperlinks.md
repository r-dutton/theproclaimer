[web] PUT /api/binders/{binderId:guid}/hyperlinks/bulk-update  (Workpapers.Next.API.Controllers.Workpapers.BinderHyperlinksController.BulkUpdateHyperlinks)  [L55–L63] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request BulkUpdateBinderHyperlinksCommand [L60]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.WorkpaperRecords.BulkUpdateBinderHyperlinksCommandHandler.Handle [L36–L267]
      └─ maps_to RecordStatusDto [L230]
        └─ automapper.registration ExternalApiMappingProfile (RecordStatus->RecordStatusDto) [L158]
        └─ automapper.registration WorkpapersMappingProfile (RecordStatus->RecordStatusDto) [L400]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L64]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Hyperlink> (AddScoped)
        └─ method WriteQuery [L79]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<RecordStatus> (AddScoped)
        └─ method ReadQuery [L230]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method WriteQuery [L172]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L232]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L70]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

