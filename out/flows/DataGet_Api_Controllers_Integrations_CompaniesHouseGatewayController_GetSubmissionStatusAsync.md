[web] GET /api/companies-house-gateway/submission-status  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.GetSubmissionStatusAsync)  [L69–L73] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetSubmissionStatusQuery [L72]
    └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Queries.GetSubmissionStatusQueryHandler.Handle [L43–L71]
      └─ maps_to SubmissionStatusResponseDto [L69]
      └─ uses_client CompaniesHouseInputGatewayClient [L65]
      └─ uses_service CompaniesHouseInputGatewayClient
        └─ method SendAsync [L65]
          └─ implementation DataGet.Integrations.CompaniesHouseGateway.Api.Gateway.CompaniesHouseInputGatewayClient.SendAsync [L13-L227]
      └─ uses_service GovTalkEnvelopeCreator
        └─ method Create [L64]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L69]
          └─ ... (no implementation details available)

