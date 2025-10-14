[web] GET /api/reportsettings/policies/{id}  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.GetPolicy)  [L46–L56]
  └─ maps_to Policy [L49]
    └─ converts_to PolicyDto [L789]
    └─ converts_to PolicyLightDto [L792]
    └─ converts_to PolicyLightWithSuiteVariantsDto [L794]
    └─ converts_to Policy [L18]
  └─ uses_service UnitOfWork
    └─ method Table [L49]
  └─ uses_service UserService
    └─ method GetFirmId [L52]

