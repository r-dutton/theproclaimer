[web] GET /api/companies-house-gateway/charge-search  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.GetChargeSearchAsync)  [L58–L62] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetChargeSearchQuery -> GetChargeSearchQueryHandler [L61]
    └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Queries.GetChargeSearchQueryHandler.Handle [L51–L80]
      └─ maps_to ChargesResponseDto [L78]
      └─ uses_client CompaniesHouseInputGatewayClient [L74]
      └─ uses_service CompaniesHouseInputGatewayClient
        └─ method SendAsync [L74]
          └─ implementation DataGet.Integrations.CompaniesHouseGateway.Api.Gateway.CompaniesHouseInputGatewayClient.SendAsync [L13-L227]
      └─ uses_service GovTalkEnvelopeCreator
        └─ method Create [L73]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ clients 1
      └─ CompaniesHouseInputGatewayClient
    └─ requests 1
      └─ GetChargeSearchQuery
    └─ handlers 1
      └─ GetChargeSearchQueryHandler
    └─ mappings 1
      └─ ChargesResponseDto

