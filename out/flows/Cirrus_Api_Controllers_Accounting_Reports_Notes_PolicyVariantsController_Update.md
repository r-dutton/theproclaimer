[web] PUT /api/accounting/reports/notes/policies/variants/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyVariantsController.Update)  [L122–L130] [auth=Authentication.UserPolicy]
  └─ calls PolicyVariantRepository.WriteQuery [L125]
  └─ calls ReportContentRepository.LoadWriteProperties [L127]
  └─ writes_to PolicyVariant [L125]
    └─ reads_from PolicyVariants
  └─ uses_service IControlledRepository<PolicyVariant>
    └─ method WriteQuery [L125]
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadWriteProperties [L127]

