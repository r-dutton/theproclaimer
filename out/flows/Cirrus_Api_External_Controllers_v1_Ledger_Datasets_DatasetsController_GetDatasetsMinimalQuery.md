[web] GET /ledger/v1/datasets/  (Cirrus.Api.External.Controllers.v1.Ledger.Datasets.DatasetsController.GetDatasetsMinimalQuery)  [L45–L52]
  └─ calls DatasetRepository.ReadQuery [L48]
  └─ queries Dataset [L48]
  └─ uses_service IControlledRepository<Dataset>
    └─ method ReadQuery [L48]

