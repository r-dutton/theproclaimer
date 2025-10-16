[web] GET /api/workpaper-records/external-values/evaluate  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.EvaluateExternalValue)  [L244–L248] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request EvaluateExternalValueQuery -> EvaluateExternalValueQueryHandler [L247]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.WorkpaperRecords.ExternalValues.EvaluateExternalValueQueryHandler.Handle [L54–L126]
      └─ uses_service ICirrusQueryService (AddScoped)
        └─ method GetDataset [L121]
          └─ implementation Workpapers.Next.CirrusServices.CirrusQueryService.GetDataset [L18-L250]
            └─ uses_service CirrusHttp
              └─ method GetAccounts [L33]
                └─ ... (no implementation details available)
            └─ uses_service CirrusConfig
              └─ method GetAccounts [L31]
                └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L110]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L104]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
      └─ uses_service IControlledRepository<Worksheet> (Scoped (inferred))
        └─ method ReadQuery [L89]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorksheetRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ EvaluateExternalValueQuery
    └─ handlers 1
      └─ EvaluateExternalValueQueryHandler

