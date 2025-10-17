[web] GET /api/companies-house-gateway/status-acknowledgement  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.GetStatusAcknowledgementAsync)  [L80–L84] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetStatusAcknowledgementQuery -> GetStatusAcknowledgementQueryHandler [L83]
    └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Queries.GetStatusAcknowledgementQueryHandler.Handle [L25–L53]
      └─ maps_to StatusAckDto [L51]
      └─ uses_client CompaniesHouseInputGatewayClient [L47]
      └─ uses_service CompaniesHouseInputGatewayClient
        └─ method SendAsync [L47]
          └─ implementation DataGet.Integrations.CompaniesHouseGateway.Api.Gateway.CompaniesHouseInputGatewayClient.SendAsync [L13-L227]
      └─ uses_service GovTalkEnvelopeCreator
        └─ method Create [L46]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ clients 1
      └─ CompaniesHouseInputGatewayClient
    └─ requests 1
      └─ GetStatusAcknowledgementQuery
    └─ handlers 1
      └─ GetStatusAcknowledgementQueryHandler
    └─ mappings 1
      └─ StatusAckDto

