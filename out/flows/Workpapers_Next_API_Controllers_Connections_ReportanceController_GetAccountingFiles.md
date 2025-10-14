[web] GET /api/connections/reportance/files  (Workpapers.Next.API.Controllers.Connections.ReportanceController.GetAccountingFiles)  [L28–L34]
  └─ sends_request GetFilesQuery [L31]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.CirrusServices.Queries.GetFilesQueryHandler.Handle [L12–L39]
      └─ uses_service CirrusConfig
        └─ method GetBaseUrl [L27]
      └─ uses_service CirrusHttp
        └─ method GetHttpResponseAsync [L35]
      └─ uses_service IMapToNew<FileSearchResult, AccountingFileDto>
        └─ method Map [L37]

