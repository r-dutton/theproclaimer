[web] POST /api/companies-house-gateway/accounts  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.SubmitAccountsAsync)  [L123–L128] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request SubmitAccountsCommand [L127]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Commands.SubmitAccountsCommandHandler.Handle [L89–L132]
      └─ uses_client CompaniesHouseInputGatewayClient [L106]
      └─ uses_service CompaniesHouseInputGatewayClient
        └─ method SendAsync [L106]
      └─ uses_service GovTalkEnvelopeCreator
        └─ method CreateFiling [L105]

