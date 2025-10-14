[web] POST /api/companies-house/test-data/company  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.CreateSandboxTextCompany)  [L432–L440] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request CreateSandboxTestCompanyCommand [L437]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Commands.CreateTestCompanyCommandHandler.Handle [L14–L21]

