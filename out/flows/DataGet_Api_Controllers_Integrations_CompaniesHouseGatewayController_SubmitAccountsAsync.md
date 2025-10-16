[web] POST /api/companies-house-gateway/accounts  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.SubmitAccountsAsync)  [L123–L128] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request SubmitAccountsCommand [L127]
    └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Commands.SubmitAccountsCommandHandler.Handle [L89–L132]
      └─ uses_client CompaniesHouseInputGatewayClient [L106]
      └─ uses_service CompaniesHouseInputGatewayClient
        └─ method SendAsync [L106]
          └─ implementation DataGet.Integrations.CompaniesHouseGateway.Api.Gateway.CompaniesHouseInputGatewayClient.SendAsync [L13-L227]
      └─ uses_service GovTalkEnvelopeCreator
        └─ method CreateFiling [L105]
          └─ ... (no implementation details available)

