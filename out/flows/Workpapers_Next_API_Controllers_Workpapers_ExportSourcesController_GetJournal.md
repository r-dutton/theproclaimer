[web] GET /api/sources/export/{id:guid}/journal-for-binder/{binderId:guid}  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.GetJournal)  [L61–L66] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetExportSourceJournalQuery [L64]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceData.ExportSources.GetExportSourceJournalQueryHandler.Handle [L38–L265]
      └─ maps_to SourceAccountUltraLightDto [L139]
        └─ automapper.registration CirrusMappingProfile (SourceAccount->SourceAccountUltraLightDto) [L260]
        └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountUltraLightDto) [L615]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L90]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<ExportSource>
        └─ method ReadQuery [L69]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceAccount>
        └─ method ReadQuery [L108]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L141]
          └─ ... (no implementation details available)
      └─ uses_service ISwingingAccountService (AddScoped)
        └─ method GetSwingingAccounts [L80]
          └─ implementation Workpapers.Next.Services.SwingingAccounts.SwingingAccountService.GetSwingingAccounts [L14-L91]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L106]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

