[web] GET /api/connections/myob/es/files  (Workpapers.Next.API.Controllers.Connections.MYOBEssentialsController.GetAccountingFiles)  [L26–L32]
  └─ sends_request GetBusinessesQuery [L29]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.MyobEssentialsServices.Queries.GetBusinessesQueryHandler.Handle [L10–L29]
      └─ uses_service IMapToNew<Business, AccountingFileDto>
        └─ method Map [L27]
      └─ uses_service MyobEssentialsHttp
        └─ method GetHttpResponseAsync [L24]

