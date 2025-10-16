[web] GET /api/sources/{type}/taxonomy  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetTaxonomy)  [L441–L447] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetBinderTaxonomyQuery -> GetBinderTaxonomyQueryHandler [L444]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Binders.GetBinderTaxonomyQueryHandler.Handle [L32–L113]
      └─ calls SourceAccountRepository.ReadQuery [L92]
      └─ uses_service ConnectionApiService
        └─ method GetApiMethods [L86]
          └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods [L20-L75]
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L58]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L56]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ requests 1
      └─ GetBinderTaxonomyQuery
    └─ handlers 1
      └─ GetBinderTaxonomyQueryHandler

