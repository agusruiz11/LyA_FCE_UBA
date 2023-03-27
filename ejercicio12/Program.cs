const double iva = 21;
const double gastosIndirectos = 100000;

string codigoArticulo;//String no vacio
string nombreArticulo;//String no vacio
double costoPorKg; // Mayor a 0
double gramosPorUnidad; // Mayor a 0
double porcentajeIndirectos; // Entre 0 y 100
double porcentajeRentabilidad; // Puede ser negativo, rentabilidad 0 (vendo al costo) o mayor a 100 (rentabilidad 200% por ejemplo)

double precioFinal; //Es calculado por el sistema
double costoTotal; //Es el costo
string codigoArticuloMasBarato, codigoArticuloMasCaro, codigoArticuloMasRentable;
string nombreArticuloMasBarato, nombreArticuloMasCaro, nombreArticuloMasRentable;
double precioMinimo, precioMaximo, rentabilidadMaxima;
