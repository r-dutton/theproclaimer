[web] POST /api/dataverse/firms  (Workpapers.Next.API.Controllers.DataverseController.CreateFirm)  [L234–L267] status=201 [auth=AuthorizationPolicies.M2M]
  └─ maps_to FirmLightWithSubscriptionsDto [L266]
  └─ maps_to FirmCreateModel (var firmModel) [L254]
    └─ automapper.registration DataverseMappingProfile (DataverseFirmCreateModel->FirmCreateModel) [L111]
  └─ uses_service IMapper
    └─ method Map [L254]
      └─ ... (no implementation details available)
  └─ uses_service UnitOfWork
    └─ method Table [L241]
      └─ ... (no implementation details available)

