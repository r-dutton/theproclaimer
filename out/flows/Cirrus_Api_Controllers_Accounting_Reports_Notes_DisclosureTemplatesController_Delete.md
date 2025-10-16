[web] DELETE /api/accounting/reports/notes/disclosure-templates/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.Delete)  [L180–L187] status=200 [auth=Authentication.UserPolicy]
  └─ calls DisclosureTemplateRepository.Remove [L185]
  └─ calls DisclosureTemplateRepository.WriteQuery [L183]
  └─ delete DisclosureTemplate [L185]
    └─ reads_from DisclosureTemplates
  └─ write DisclosureTemplate [L183]
    └─ reads_from DisclosureTemplates
  └─ uses_service IControlledRepository<DisclosureTemplate>
    └─ method WriteQuery [L183]
      └─ ... (no implementation details available)

