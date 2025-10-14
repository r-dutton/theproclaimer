[web] GET /api/connections/reportance/clientdetails/from-dataset  (Workpapers.Next.API.Controllers.Connections.ReportanceController.GetClientDetails)  [L52–L58]
  └─ sends_request GetClientDetailsQuery [L55]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.CirrusServices.Queries.GetClientDetailsQueryHandler.Handle [L42–L117]
      └─ uses_service CirrusConfig
        └─ method GetBaseUrl [L63]
      └─ uses_service CirrusHttp
        └─ method GetHttpResponseAsync [L64]

