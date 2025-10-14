[web] GET /api/users  (Workpapers.Next.API.Controllers.UsersController.GetAllMyFirm)  [L72–L89]
  └─ sends_request GetUsersQuery [L86]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Fyi.Api.Queries.GetUsersQueryHandler.Handle [L19–L36]
      └─ uses_service FyiService
        └─ method GetUsers [L30]
  └─ sends_request GetPagedUsersQuery [L88]

