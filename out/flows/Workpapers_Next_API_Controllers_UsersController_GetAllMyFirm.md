[web] GET /api/users  (Workpapers.Next.API.Controllers.UsersController.GetAllMyFirm)  [L72–L89] status=200
  └─ sends_request GetPagedUsersQuery [L88]
  └─ sends_request GetUsersQuery -> GetUsersQueryHandler [L86]
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetUsersQueryHandler.Handle [L19–L36]
      └─ uses_service FyiService
        └─ method GetUsers [L30]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetUsers [L30-L166]
            └─ uses_client FyiHttpClient [L50]
            └─ uses_service FyiHttpClient
              └─ method GetCabinets [L50]
                └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets [L24-L194]
  └─ impact_summary
    └─ clients 1
      └─ FyiHttpClient
    └─ requests 2
      └─ GetPagedUsersQuery
      └─ GetUsersQuery
    └─ handlers 1
      └─ GetUsersQueryHandler

