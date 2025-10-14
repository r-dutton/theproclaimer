[web] GET /api/companies-house-gateway/submission-status  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.GetSubmissionStatusAsync)  [L69–L73] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetSubmissionStatusQuery [L72]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Queries.GetSubmissionStatusQueryHandler.Handle [L43–L71]
      └─ maps_to SubmissionStatusResponseDto [L69]
      └─ uses_client CompaniesHouseInputGatewayClient [L65]
      └─ uses_service CompaniesHouseInputGatewayClient
        └─ method SendAsync [L65]
      └─ uses_service GovTalkEnvelopeCreator
        └─ method Create [L64]
      └─ uses_service IMapper
        └─ method Map [L69]

