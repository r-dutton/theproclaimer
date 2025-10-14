[web] DELETE /api/accounting/reports/notes/disclosure-templates/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.Delete)  [L180–L187] [auth=Authentication.UserPolicy]
  └─ calls DisclosureTemplateRepository.Remove [L185]
  └─ calls DisclosureTemplateRepository.WriteQuery [L183]
  └─ writes_to DisclosureTemplate [L185]
    └─ reads_from DisclosureTemplates
  └─ writes_to DisclosureTemplate [L183]
    └─ reads_from DisclosureTemplates
  └─ uses_service IControlledRepository<DisclosureTemplate>
    └─ method WriteQuery [L183]

