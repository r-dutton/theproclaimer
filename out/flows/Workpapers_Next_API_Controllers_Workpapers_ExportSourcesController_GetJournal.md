[web] GET /api/sources/export/{id:guid}/journal-for-binder/{binderId:guid}  (Workpapers.Next.API.Controllers.Workpapers.ExportSourcesController.GetJournal)  [L61–L66] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetExportSourceJournalQuery -> GetExportSourceJournalQueryHandler [L64]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.SourceData.ExportSources.GetExportSourceJournalQueryHandler.Handle [L38–L265]
      └─ calls SourceAccountRepository.ReadQuery [L139]
      └─ maps_to SourceAccountUltraLightDto [L139]
        └─ automapper.registration WorkpapersMappingProfile (SourceAccount->SourceAccountUltraLightDto) [L615]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L106]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L90]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
      └─ uses_service ISwingingAccountService (AddScoped)
        └─ method GetSwingingAccounts [L80]
          └─ implementation Workpapers.Next.Services.SwingingAccounts.SwingingAccountService.GetSwingingAccounts [L14-L91]
      └─ uses_service IControlledRepository<ExportSource> (Scoped (inferred))
        └─ method ReadQuery [L69]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.ExportSourceRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetExportSourceJournalQuery
    └─ handlers 1
      └─ GetExportSourceJournalQueryHandler
    └─ mappings 1
      └─ SourceAccountUltraLightDto

