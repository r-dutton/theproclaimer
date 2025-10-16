[web] GET /api/users  (Workpapers.Next.API.Controllers.UsersController.GetAllMyFirm)  [L72–L89] status=200
  └─ sends_request GetUsersQuery [L86]
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetUsersQueryHandler.Handle [L19–L36]
      └─ uses_service FyiService
        └─ method GetUsers [L30]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.GetUsers [L30-L166]
  └─ sends_request GetPagedUsersQuery [L88]

