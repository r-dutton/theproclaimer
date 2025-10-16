[web] GET /api/connections/myob/es/files  (Workpapers.Next.API.Controllers.Connections.MYOBEssentialsController.GetAccountingFiles)  [L26–L32] status=200
  └─ sends_request GetBusinessesQuery [L29]
    └─ handled_by Workpapers.Next.MyobEssentialsServices.Queries.GetBusinessesQueryHandler.Handle [L10–L29]
      └─ uses_service IMapToNew<Business, AccountingFileDto>
        └─ method Map [L27]
          └─ ... (no implementation details available)
      └─ uses_service MyobEssentialsHttp
        └─ method GetHttpResponseAsync [L24]
          └─ ... (no implementation details available)

