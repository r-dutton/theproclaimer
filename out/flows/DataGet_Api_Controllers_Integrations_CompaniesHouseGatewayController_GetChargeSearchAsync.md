[web] GET /api/companies-house-gateway/charge-search  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.GetChargeSearchAsync)  [L58–L62] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetChargeSearchQuery [L61]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Queries.GetChargeSearchQueryHandler.Handle [L51–L80]
      └─ maps_to ChargesResponseDto [L78]
      └─ uses_client CompaniesHouseInputGatewayClient [L74]
      └─ uses_service CompaniesHouseInputGatewayClient
        └─ method SendAsync [L74]
      └─ uses_service GovTalkEnvelopeCreator
        └─ method Create [L73]
      └─ uses_service IMapper
        └─ method Map [L78]

