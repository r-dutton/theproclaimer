[web] DELETE /api/companies-house/test-data/company/{companyNumber}  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.DeleteSandboxTextCompany)  [L442–L450] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request DeleteSandboxTestCompanyCommand [L447]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Commands.DeleteTestCompanyCommandHandler.Handle [L16–L26]

