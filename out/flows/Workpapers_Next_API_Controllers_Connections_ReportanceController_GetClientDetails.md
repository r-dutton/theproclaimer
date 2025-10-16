[web] GET /api/connections/reportance/clientdetails/from-dataset  (Workpapers.Next.API.Controllers.Connections.ReportanceController.GetClientDetails)  [L52–L58] status=200
  └─ sends_request GetClientDetailsQuery [L55]
    └─ handled_by Workpapers.Next.CirrusServices.Queries.GetClientDetailsQueryHandler.Handle [L42–L117]
      └─ uses_service CirrusConfig
        └─ method GetBaseUrl [L63]
          └─ ... (no implementation details available)
      └─ uses_service CirrusHttp
        └─ method GetHttpResponseAsync [L64]
          └─ ... (no implementation details available)

