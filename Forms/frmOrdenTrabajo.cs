// Archivo: Forms/frmOrdenTrabajo.cs
using System.Text.RegularExpressions;
using System.Diagnostics;
using LubricentroVelezV2.DTOs;
using LubricentroVelezV2.Services.Implementation; // Asegúrate de incluir el using del Service
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LubricentroVelezV2.Forms
{
    public partial class frmOrdenTrabajo : Form
    {
        private readonly OrdenesService _service;
        private OrdenTrabajoDetailsDTO _ordenActual;
        private List<OrdenesService.AceiteComboItem> _aceites;
        private List<OrdenesService.AditivoComboItem> _aditivos;
        private const int NEW_ORDER_ID = 0;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        // Delegado para notificar al formulario principal
        public event EventHandler DataChanged;

        // Constructor para NUEVA Orden
        public frmOrdenTrabajo(OrdenesService service)
        {
            InitializeComponent();
            _service = service;
            _ordenActual = new OrdenTrabajoDetailsDTO { IdOt = NEW_ORDER_ID };
            SetupForm(isNew: true);
            this.Load += async (s, e) => await LoadCombosAsync();
            this.txtPatente.TextChanged += txtPatente_TextChanged;
            this.btnGuardar.Click += btnGuardar_Click;
            this.btnCancelar.Click += (s, e) => this.Close();
            this.btnEditar.Click += btnEditar_Click;
            this.btnContactar.Click += btnContactar_Click;
        }

        // Constructor para VER/EDITAR Orden existente
        public frmOrdenTrabajo(OrdenesService service, int idOt)
        {
            InitializeComponent();
            _service = service;
            _ordenActual = new OrdenTrabajoDetailsDTO { IdOt = idOt }; // El DTO se rellenará en Load
            SetupForm(isNew: false);
            this.Load += async (s, e) => await LoadOrderDataAsync(idOt);
            this.txtPatente.TextChanged += txtPatente_TextChanged;
            this.btnGuardar.Click += btnGuardar_Click;
            this.btnCancelar.Click += (s, e) => this.Close();
            this.btnEditar.Click += btnEditar_Click;
            this.btnContactar.Click += btnContactar_Click;
        }

        private void SetupForm(bool isNew)
        {
            if (isNew)
            {
                this.Text = "Nueva Orden de Trabajo";
                btnGuardar.Visible = true;
                btnGuardar.Enabled = true;
                btnCancelar.Visible = true;
                btnCancelar.Enabled = true;
                btnEditar.Visible = false;
                btnContactar.Visible = false;
                EnableControls(true);
            }
            else
            {
                this.Text = $"Orden de Trabajo N° {_ordenActual.IdOt}";
                btnGuardar.Visible = true;
                btnGuardar.Enabled = false; // Deshabilitado inicialmente
                btnCancelar.Visible = true;
                btnCancelar.Enabled = true;
                btnEditar.Visible = true;
                btnEditar.Enabled = true; // Habilitado
                btnContactar.Visible = true;
                EnableControls(false); // Deshabilitado para solo ver
            }
        }

        // Habilita o deshabilita los controles de entrada
        private void EnableControls(bool enable)
        {
            txtPatente.ReadOnly = !enable;
            txtPropietario.ReadOnly = !enable;
            txtTelefono.ReadOnly = !enable;
            txtAutomovil.ReadOnly = !enable;
            txtModelo.ReadOnly = !enable;
            // OJO: textBox1 es el Kilometraje
            txtKilometraje.ReadOnly = !enable;

            cmbAceites.Enabled = enable;
            cmbAditivos.Enabled = enable;

            chkFAceite.Enabled = enable;
            chkFAire.Enabled = enable;
            chkFCom.Enabled = enable;
            chkFHabi.Enabled = enable;

            txtObservaciones.ReadOnly = !enable;
        }

        // --- Carga de Datos y Autocompletado ---

        private async Task LoadCombosAsync()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                _aceites = await _service.GetAceitesForComboAsync();
                _aditivos = await _service.GetAditivosForComboAsync();

                // Configurar Aceites
                cmbAceites.DataSource = _aceites;
                cmbAceites.DisplayMember = "Display";
                cmbAceites.ValueMember = "IdAceite";

                // Configurar Aditivos
                cmbAditivos.DataSource = _aditivos;
                cmbAditivos.DisplayMember = "Display";
                cmbAditivos.ValueMember = "IdAditivo";

                // Seleccionar "Ninguno" por defecto
                cmbAceites.SelectedIndex = -1;
                cmbAditivos.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar combos: {ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async Task LoadOrderDataAsync(int idOt)
        {
            await LoadCombosAsync();

            this.Cursor = Cursors.WaitCursor;
            try
            {
                _ordenActual = await _service.GetOrderDetailsByIdAsync(idOt);
                if (_ordenActual == null)
                {
                    MessageBox.Show("Orden de trabajo no encontrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Rellenar Controles
                txtPatente.Text = _ordenActual.Patente;
                txtPropietario.Text = _ordenActual.PropietarioNombre;
                txtTelefono.Text = _ordenActual.Telefono;
                txtAutomovil.Text = _ordenActual.Automovil;
                txtModelo.Text = _ordenActual.Modelo?.ToString();
                txtKilometraje.Text = _ordenActual.Kilometraje?.ToString();

                // Combos
                cmbAceites.SelectedValue = _ordenActual.IdAceite.HasValue ? _ordenActual.IdAceite.Value : -1;
                cmbAditivos.SelectedValue = _ordenActual.IdAditivo.HasValue ? _ordenActual.IdAditivo.Value : -1;

                // CheckBoxes
                chkFAceite.Checked = _ordenActual.FiltroAceite ?? false;
                chkFAire.Checked = _ordenActual.FiltroAire ?? false;
                chkFCom.Checked = _ordenActual.FiltroCombustible ?? false;
                chkFHabi.Checked = _ordenActual.FiltroAbitaculo ?? false;

                txtObservaciones.Text = _ordenActual.Observaciones;

                this.Text = $"Orden de Trabajo N° {_ordenActual.IdOt}";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la Orden: {ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async void txtPatente_TextChanged(object? sender, EventArgs e)
        {
            // Solo se autocompleta si es una orden nueva
            if (_ordenActual.IdOt != NEW_ORDER_ID) return;

            string patente = txtPatente.Text.Trim().ToUpper();

            // 1. Lógica para limpiar campos si la patente está vacía o incompleta.
            if (string.IsNullOrWhiteSpace(patente) || patente.Length < 3) // Usar un largo mínimo (ej. 3)
            {
                ClearVehicleFields();
                // Cancelar cualquier operación pendiente si se está borrando
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
                return;
            }

            // 2. Manejo de Concurrencia (Cancelar operación anterior)
            // Cancelamos la fuente anterior y creamos una nueva
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            // 3. Debounce: Esperar un momento antes de buscar (ej. 300ms)
            try
            {
                await Task.Delay(300, token);
            }
            catch (TaskCanceledException)
            {
                // Si la tarea se canceló (por nueva tecla), salimos silenciosamente
                return;
            }

            // 4. Iniciar la Búsqueda Asíncrona
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (token.IsCancellationRequested) return; // Chequeo de cancelación

                var lastData = await _service.GetLastOrderDataByPatenteAsync(patente);

                if (lastData != null)
                {
                    // Autocompletar campos
                    txtPropietario.Text = lastData.PropietarioNombre;
                    txtTelefono.Text = lastData.Telefono;
                    txtAutomovil.Text = lastData.Automovil;
                    txtModelo.Text = lastData.Modelo?.ToString();

                    cmbAceites.SelectedValue = lastData.IdAceite.HasValue ? lastData.IdAceite.Value : -1;
                    cmbAditivos.SelectedValue = lastData.IdAditivo.HasValue ? lastData.IdAditivo.Value : -1;
                }
                else
                {
                    // Si no se encuentra, limpiar los campos autocompletados (sólo si se está ingresando algo)
                    ClearVehicleFields();
                }
            }
            catch (TaskCanceledException)
            {
                // Ignorar la excepción si fue causada por la cancelación
            }
            catch (Exception ex)
            {
                // El error de concurrencia debería estar resuelto, este sería otro error de BD
                MessageBox.Show($"Error de autocompletado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ClearVehicleFields()
        {
            txtPropietario.Clear();
            txtTelefono.Clear();
            txtAutomovil.Clear();
            txtModelo.Clear();
            // Restablecer combos a nada seleccionado
            cmbAceites.SelectedIndex = -1;
            cmbAditivos.SelectedIndex = -1;
        }

        // --- Guardar y Editar ---

        private OrdenTrabajoDetailsDTO GetFormData()
        {
            // Validaciones básicas de campos obligatorios
            if (string.IsNullOrWhiteSpace(txtPatente.Text))
            {
                throw new InvalidOperationException("La Patente es obligatoria.");
            }
            if (string.IsNullOrWhiteSpace(txtPropietario.Text))
            {
                throw new InvalidOperationException("El Propietario es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(txtKilometraje.Text) || !double.TryParse(txtKilometraje.Text, out double kilometraje))
            {
                throw new InvalidOperationException("El Kilometraje actual es obligatorio y debe ser un número.");
            }

            int? modelo = int.TryParse(txtModelo.Text, out int m) ? (int?)m : null;
            int? idAceite = cmbAceites.SelectedValue != null && (int)cmbAceites.SelectedValue != -1 ? (int)cmbAceites.SelectedValue : null;
            int? idAditivo = cmbAditivos.SelectedValue != null && (int)cmbAditivos.SelectedValue != -1 ? (int)cmbAditivos.SelectedValue : null;

            return new OrdenTrabajoDetailsDTO
            {
                IdOt = _ordenActual.IdOt,
                Patente = txtPatente.Text.Trim().ToUpper(),
                PropietarioNombre = txtPropietario.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Automovil = txtAutomovil.Text.Trim(),
                Modelo = modelo,
                Kilometraje = kilometraje,
                IdAceite = idAceite,
                IdAditivo = idAditivo,
                FiltroAceite = chkFAceite.Checked,
                FiltroAire = chkFAire.Checked,
                FiltroCombustible = chkFCom.Checked,
                FiltroAbitaculo = chkFHabi.Checked,
                Observaciones = txtObservaciones.Text.Trim()
            };
        }

        private async void btnGuardar_Click(object? sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            try
            {
                var ordenData = GetFormData();

                if (_ordenActual.IdOt == NEW_ORDER_ID)
                {
                    // Guardar nueva orden
                    int newId = await _service.AddOrdenTrabajoAsync(ordenData);
                    MessageBox.Show($"Orden de Trabajo N° {newId} guardada con éxito.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Guardar edición
                    await _service.UpdateOrdenTrabajoAsync(ordenData);
                    MessageBox.Show($"Orden de Trabajo N° {_ordenActual.IdOt} actualizada con éxito.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Al editar, se vuelve al modo de solo lectura.
                    SetupForm(isNew: false);
                }

                DataChanged?.Invoke(this, EventArgs.Empty); // Notificar al Principal
                this.Close(); // Cerrar al guardar

            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la Orden: {ex.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnEditar_Click(object? sender, EventArgs e)
        {
            // Habilita los controles para edición
            EnableControls(true);

            // Ajusta la visibilidad y estado de los botones
            btnEditar.Enabled = false;
            btnGuardar.Enabled = true;
            btnContactar.Enabled = true;
        }

        private void btnContactar_Click(object? sender, EventArgs e)
        {
            string telefono = txtTelefono.Text.Trim();

            // 1. Limpieza a solo dígitos (Mantenemos la validación estricta)
            string soloDigitos = System.Text.RegularExpressions.Regex.Replace(telefono, "[^0-9]", "");

            // 2. Validación estricta
            if (soloDigitos == "0" || soloDigitos.Length < 10 || soloDigitos.Length > 12)
            {
                MessageBox.Show(
                    "El número es demasiado corto, es '0' o tiene un formato incorrecto para contacto.",
                    "Error de Contacto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (soloDigitos == "012345678" || System.Text.RegularExpressions.Regex.IsMatch(soloDigitos, @"^(\d)\1+$"))
            {
                MessageBox.Show(
                   "Este número parece ser un marcador de posición (ej. 012345678) y no es válido para WhatsApp.",
                   "Error de Contacto",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning
               );
                return;
            }

            if (!soloDigitos.StartsWith("351"))
            {
                MessageBox.Show(
                    "El número no comienza con el código de área esperado (351). Verifique si es válido.",
                    "Advertencia de Formato",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }


            // 3. Formateo al estándar Internacional
            // Código de País para Argentina: 54
            string numeroInternacional = "54" + soloDigitos;


            // 4. Construcción del Mensaje Personalizado
            string nombreCliente = txtPropietario.Text.Trim();
            string automovilCliente = txtAutomovil.Text.Trim();

            // El mensaje usa el salto de línea \n
            string mensajeRaw = $"Hola {nombreCliente}, Lubricentro Velez te recuerda que tenés que cambiarle el aceite a tu {automovilCliente} cada 10.000km";

            // 5. Codificación URL del mensaje
            // Esto convierte espacios en %20 y saltos de línea (\n) en %0A, algo requerido por la URL de WhatsApp.
            string mensajeCodificado = Uri.EscapeDataString(mensajeRaw);

            // 6. Crear la URL de WhatsApp con el parámetro 'text'
            string urlWhatsApp = $"https://wa.me/{numeroInternacional}?text={mensajeCodificado}";

            // 7. Ejecutar la acción
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(urlWhatsApp) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"No se pudo abrir WhatsApp. Error: {ex.Message}",
                    "Error al Contactar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                // Disponer del CancellationTokenSource
                _cancellationTokenSource?.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool ValidarCampos()
        {
            var errores = new System.Text.StringBuilder();

            // ----------------------------------------------------
            // 1. Patente (Requerido, Letras Y Números)
            // ----------------------------------------------------
            string patente = txtPatente.Text.Trim();
            if (string.IsNullOrWhiteSpace(patente))
            {
                errores.AppendLine("- La Patente es requerida.");
            }
            else if (!Regex.IsMatch(patente, @"[a-zA-Z]") || !Regex.IsMatch(patente, @"[0-9]"))
            {
                // Verifica que contenga al menos una letra Y al menos un número
                errores.AppendLine("- La Patente debe contener letras y números.");
            }

            // ----------------------------------------------------
            // 2. Propietario (Requerido, Solo Letras)
            // ----------------------------------------------------
            string propietario = txtPropietario.Text.Trim();
            // Expresión: Solo letras (incluye acentos y la ñ, y espacios)
            if (string.IsNullOrWhiteSpace(propietario))
            {
                errores.AppendLine("- El Propietario es requerido.");
            }
            else if (!Regex.IsMatch(propietario, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
            {
                errores.AppendLine("- El Propietario solo puede contener letras.");
            }

            // ----------------------------------------------------
            // 3. Teléfono (Requerido, Solo Números)
            // ----------------------------------------------------
            string telefono = txtTelefono.Text.Trim();
            // Expresión: Solo números (incluyendo vacío, pero ya chequeamos que no sea vacío)
            if (string.IsNullOrWhiteSpace(telefono))
            {
                errores.AppendLine("- El Teléfono es requerido.");
            }
            else if (!Regex.IsMatch(telefono, @"^[0-9]+$"))
            {
                errores.AppendLine("- El Teléfono solo puede contener números.");
            }

            // ----------------------------------------------------
            // 4. Automóvil (Requerido, Letras y/o Números)
            // ----------------------------------------------------
            if (string.IsNullOrWhiteSpace(txtAutomovil.Text.Trim()))
            {
                errores.AppendLine("- El Automóvil es requerido.");
            }
            // Nota: El campo permite letras Y/O números, así que el chequeo de "no vacío" es suficiente.

            // ----------------------------------------------------
            // 5. Modelo (Requerido, Solo Números)
            // ----------------------------------------------------
            string modelo = txtModelo.Text.Trim();
            if (string.IsNullOrWhiteSpace(modelo))
            {
                errores.AppendLine("- El Modelo es requerido.");
            }
            else if (!Regex.IsMatch(modelo, @"^[0-9]+$"))
            {
                errores.AppendLine("- El Modelo solo puede contener números.");
            }

            // ----------------------------------------------------
            // 6. Kilometraje (Requerido, Solo Números)
            // ----------------------------------------------------
            string kilometraje = txtKilometraje.Text.Trim();
            if (string.IsNullOrWhiteSpace(kilometraje))
            {
                errores.AppendLine("- El Kilometraje es requerido.");
            }
            else if (!Regex.IsMatch(kilometraje, @"^[0-9]+$"))
            {
                errores.AppendLine("- El Kilometraje solo puede contener números.");
            }

            // ----------------------------------------------------
            // 7. Aceite y Aditivo (Requerido, Selección en ComboBox)
            // ----------------------------------------------------
            // Asumiendo que el valor por defecto (no seleccionado) es -1 o 0, o que el SelectedValue es null.
            // Si usas objetos en DataBinding, puedes chequear si SelectedItem es null. Si usas ValueMember, puedes chequear el SelectedValue.

            if (cmbAceites.SelectedValue == null || (int)cmbAceites.SelectedValue <= 0)
            {
                errores.AppendLine("- Debe seleccionar un Aceite.");
            }

            if (cmbAditivos.SelectedValue == null || (int)cmbAditivos.SelectedValue <= 0)
            {
                errores.AppendLine("- Debe seleccionar un Aditivo.");
            }

            // ----------------------------------------------------
            // 8. Campos NO requeridos (Checkbox, Observaciones)
            // ----------------------------------------------------
            // No se agrega lógica de error para estos campos.

            // ----------------------------------------------------
            // Resultado Final
            // ----------------------------------------------------
            if (errores.Length > 0)
            {
                MessageBox.Show("Faltan datos o contienen formato incorrecto:\n" + errores.ToString(),
                                "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}