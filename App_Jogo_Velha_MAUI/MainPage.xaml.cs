using System.Diagnostics;

namespace App_Jogo_Velha_MAUI
{
    public partial class MainPage : ContentPage
    {
        readonly int game_matrix_dimensions_length = 4;

        string[,] game_moves;

        bool game_is_on_going = true;

        int total_game_moves = 0;

        string turn = "X";

        string winner = "";

        enum Axis
        {
            Row,
            Column
        };

        public MainPage()
        {
            InitializeComponent();

            Grid.SetColumnSpan(lbl_title, this.game_matrix_dimensions_length);

            Grid.SetColumnSpan(btn_reset, this.game_matrix_dimensions_length);

            this.game_moves = new string[this.game_matrix_dimensions_length, this.game_matrix_dimensions_length];

            ResetGameMatrix();
        }

        private void DisplayCurrentFramesInDebugConsole()
        {
            Debug.WriteLine("\n========== TABULEIRO ATUAL ==========\n");

            for (int row_index = 0; row_index < this.game_moves.GetLength(0); row_index++)
            {
                for (int column_index = 0; column_index < this.game_moves.GetLength(1); column_index++)
                {
                    Debug.Write(" (" + row_index.ToString() + "|" + column_index.ToString() + "):" + this.game_moves[row_index, column_index]);
                }

                Debug.Write("\n");
            }

            Debug.WriteLine("\n Quantidade atual de jogadas: " + this.total_game_moves.ToString() + ".");
        }

        private void ResetGameButtons()
        {
            foreach (IView element in grid_game.Children)
            {
                if (element is Button btn_game)
                {
                    if (btn_game.StyleId == "btn_game")
                    {
                        btn_game.Text = "";
                    }
                }
            }
        }

        private void ResetGameMatrix()
        {
            for (int row_index = 0; row_index < this.game_moves.GetLength(0); row_index++)
            {
                for (int column_index = 0; column_index < this.game_moves.GetLength(1); column_index++)
                {
                    this.game_moves[row_index, column_index] = "";
                }
            }
        }

        private bool VerifyAxisValues(Axis dimension_selected)
        {
            for (int i = 0; i < this.game_matrix_dimensions_length; i++)
            {
                int repetition_count = 0;

                for (int j = 1; j < this.game_matrix_dimensions_length; j++)
                {
                    switch (dimension_selected)
                    {
                        case Axis.Row:
                            if (!String.IsNullOrWhiteSpace(this.game_moves[i, j - 1]) &&
                                this.game_moves[i, j - 1] == this.game_moves[i, j])
                            {
                                repetition_count++;
                            }
                        break;

                        case Axis.Column:
                            if (!String.IsNullOrWhiteSpace(this.game_moves[j - 1, i]) &&
                                this.game_moves[j - 1, i] == this.game_moves[j, i])
                            {
                                repetition_count++;
                            }
                        break;
                    }
                }

                if (repetition_count == this.game_matrix_dimensions_length - 1)
                {
                    return true;
                }
            }

            return false;
        }

        private bool VerifyDiagonalsValues()
        {
            int primary_diagonal_repetition_count = 0;

            // Verificando a diagonal principal.

            for (int k = 1; k < this.game_matrix_dimensions_length; k++)
            {
                if (!String.IsNullOrWhiteSpace(this.game_moves[k - 1, k - 1]) &&
                    this.game_moves[k - 1, k - 1] == this.game_moves[k, k])
                {
                    primary_diagonal_repetition_count++;
                }
            }

            // Verificando a diagonal secundária.

            int secondary_diagonal_repetition_count = 0;

            for (int l = 1; l < this.game_matrix_dimensions_length; l++)
            {
                if (!String.IsNullOrWhiteSpace(this.game_moves[l - 1, this.game_matrix_dimensions_length - l]) &&
                    this.game_moves[l - 1, this.game_matrix_dimensions_length - l] == this.game_moves[l, this.game_matrix_dimensions_length - l - 1])
                {
                    secondary_diagonal_repetition_count++;
                }
            }

            if (primary_diagonal_repetition_count == this.game_matrix_dimensions_length - 1 ||
                secondary_diagonal_repetition_count == this.game_matrix_dimensions_length - 1)
            {
                return true;
            }

            return false;
        }

        private async void VerifyGameStatus()
        {
            // Verificando as linhas, as colunas e as diagonais.

            if (VerifyAxisValues(Axis.Row) || VerifyAxisValues(Axis.Column) || VerifyDiagonalsValues())
            {
                this.game_is_on_going = false;

                this.winner = this.turn;

                await DisplayAlertAsync("Atenção!", ("O '" + this.winner + "' venceu."), "OK");
            }
            else if (this.total_game_moves == this.game_matrix_dimensions_length * this.game_matrix_dimensions_length)
            {
                this.game_is_on_going = false;

                this.winner = "Ninguém";

                await DisplayAlertAsync("Atenção!", this.winner + " venceu (Empate).", "OK");
            }
            else
            {
                this.turn = (this.turn == "O") ? "X" : "O";
            }
        }

        private async void btn_game_Clicked(object sender, EventArgs e)
        {
            Button event_button = (Button) sender;

            if (this.game_is_on_going && String.IsNullOrWhiteSpace(event_button.Text))
            {
                event_button.Text = turn;

                int event_button_row = Grid.GetRow(event_button) - 1;
                int event_button_column = Grid.GetColumn(event_button);

                this.game_moves[event_button_row, event_button_column] = this.turn;

                this.total_game_moves++;

                // Utilizado para fins de depuração.

                DisplayCurrentFramesInDebugConsole();

                // Verificando se alguém venceu.

                VerifyGameStatus();
            }
        }

        private void btn_reset_Clicked(object sender, EventArgs e)
        {
            this.game_is_on_going = true;

            this.total_game_moves = 0;

            this.turn = "X";

            this.winner = "";

            ResetGameButtons();

            ResetGameMatrix();
        }
    }
}
