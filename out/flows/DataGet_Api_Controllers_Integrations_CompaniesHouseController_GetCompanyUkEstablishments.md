[web] GET /api/companies-house/company/{companyNumber}/uk-establishments  (DataGet.Api.Controllers.Integrations.CompaniesHouseController.GetCompanyUkEstablishments)  [L254–L262] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyUkEstablishmentsQuery [L259]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouse.Api.Queries.GetCompanyUkEstablishmentsQueryHandler.Handle [L15–L24]

