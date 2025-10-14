[web] POST /api/dataverse/firms  (Workpapers.Next.API.Controllers.DataverseController.CreateFirm)  [L233–L266] [auth=AuthorizationPolicies.M2M]
  └─ maps_to FirmLightWithSubscriptionsDto [L265]
  └─ maps_to FirmCreateModel (var firmModel) [L253]
    └─ automapper.registration DataverseMappingProfile (DataverseFirmCreateModel->FirmCreateModel) [L111]
  └─ uses_service IMapper
    └─ method Map [L253]
  └─ uses_service UnitOfWork
    └─ method Table [L240]

