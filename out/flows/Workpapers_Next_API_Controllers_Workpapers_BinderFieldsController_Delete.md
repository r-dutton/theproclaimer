[web] DELETE /api/binder-fields/{id:Guid}  (Workpapers.Next.API.Controllers.Workpapers.BinderFieldsController.Delete)  [L105–L114] [auth=AuthorizationPolicies.SuperUser]
  └─ calls BinderFieldRepository.Remove [L111]
  └─ calls BinderFieldRepository.WriteQuery [L109]
  └─ writes_to BinderField [L111]
    └─ reads_from BinderFields
  └─ writes_to BinderField [L109]
    └─ reads_from BinderFields
  └─ uses_service IControlledRepository<BinderField>
    └─ method WriteQuery [L109]

