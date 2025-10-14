[web] PUT /api/connections/myob/es/clientdetails  (Workpapers.Next.API.Controllers.Connections.MYOBEssentialsController.GetClientDetails)  [L34–L40]
  └─ sends_request GetBusinessQuery [L37]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.MyobEssentialsServices.Queries.GetBusinessQueryHandler.Handle [L13–L41]
      └─ uses_service IMapToNew<Business, ClientDetail>
        └─ method Map [L35]
      └─ uses_service MyobEssentialsHttp
        └─ method GetHttpResponseAsync [L33]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L30]
      └─ uses_service UserService
        └─ method GetUserId [L30]

