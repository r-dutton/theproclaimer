[web] GET /api/companies-house-gateway/members-register-info  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.GetMembersRegisterDataAsync)  [L38–L42] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetMembersRegisterDataQuery [L41]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Queries.GetMembersRegisterDataQueryHandler.Handle [L33–L62]
      └─ maps_to MembersRegisterDataResponseDto [L60]
      └─ uses_client CompaniesHouseInputGatewayClient [L56]
      └─ uses_service CompaniesHouseInputGatewayClient
        └─ method SendAsync [L56]
      └─ uses_service GovTalkEnvelopeCreator
        └─ method Create [L55]
      └─ uses_service IMapper
        └─ method Map [L60]

