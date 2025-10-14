[web] GET /api/companies-house-gateway/status-acknowledgement  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.GetStatusAcknowledgementAsync)  [L80–L84] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetStatusAcknowledgementQuery [L83]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Queries.GetStatusAcknowledgementQueryHandler.Handle [L25–L53]
      └─ maps_to StatusAckDto [L51]
      └─ uses_client CompaniesHouseInputGatewayClient [L47]
      └─ uses_service CompaniesHouseInputGatewayClient
        └─ method SendAsync [L47]
      └─ uses_service GovTalkEnvelopeCreator
        └─ method Create [L46]
      └─ uses_service IMapper
        └─ method Map [L51]

