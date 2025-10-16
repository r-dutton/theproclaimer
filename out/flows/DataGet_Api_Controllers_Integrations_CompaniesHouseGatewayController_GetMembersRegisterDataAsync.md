[web] GET /api/companies-house-gateway/members-register-info  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.GetMembersRegisterDataAsync)  [L38–L42] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetMembersRegisterDataQuery [L41]
    └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Queries.GetMembersRegisterDataQueryHandler.Handle [L33–L62]
      └─ maps_to MembersRegisterDataResponseDto [L60]
      └─ uses_client CompaniesHouseInputGatewayClient [L56]
      └─ uses_service CompaniesHouseInputGatewayClient
        └─ method SendAsync [L56]
          └─ implementation DataGet.Integrations.CompaniesHouseGateway.Api.Gateway.CompaniesHouseInputGatewayClient.SendAsync [L13-L227]
      └─ uses_service GovTalkEnvelopeCreator
        └─ method Create [L55]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L60]
          └─ ... (no implementation details available)

