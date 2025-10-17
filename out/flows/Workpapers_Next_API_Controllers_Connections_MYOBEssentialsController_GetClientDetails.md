[web] PUT /api/connections/myob/es/clientdetails  (Workpapers.Next.API.Controllers.Connections.MYOBEssentialsController.GetClientDetails)  [L34–L40] status=200
  └─ sends_request GetBusinessQuery -> GetBusinessQueryHandler [L37]
    └─ handled_by Workpapers.Next.MyobEssentialsServices.Queries.GetBusinessQueryHandler.Handle [L13–L41]
      └─ uses_service IMapToNew<Business, ClientDetail>
        └─ method Map [L35]
          └─ ... (no implementation details available)
      └─ uses_service MyobEssentialsHttp
        └─ method GetHttpResponseAsync [L33]
          └─ ... (no implementation details available)
      └─ uses_service UserService
        └─ method GetUserId [L30]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L30]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ requests 1
      └─ GetBusinessQuery
    └─ handlers 1
      └─ GetBusinessQueryHandler
    └─ caches 1
      └─ IMemoryCache

