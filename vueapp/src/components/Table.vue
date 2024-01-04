<template>
    <div class="p-10">
        <table>
            <thead>
                <tr>
                    <th v-for="(column, index) in columns"
                        v-bind:key="index"
                        class="border-2 p-2 text-left"
                        v-on:click="sortRecords(index)">
                        {{column}}
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(row, index) in rows"
                    v-bind:key="index">
                    <td v-for="(rowItem, itemIndex) in row"
                        v-bind:key="itemIndex"
                        class="border-2 p-2">
                        {{rowItem}}
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
export default {
  name: 'Table',
  data () {
    return {
      term: '',
      rawRows: [
        [
           "3fa85f64-5717-4562-b3fc-2c963f66afa6", "John Wick", "2024-01-01 22:54:41.177 +02", "CALL_STARTED", "7fcffc7d-57aa-42d9-ad0b-4cae8bb0bee9","ON_CALL"
        ],
        [
           "66efe02d-dcb6-487c-8d81-bd40126338cd", "Rachel Green", "2024-01-03 22:54:41.177 +02", "CALL_STARTED", "3fa85f64-5717-4562-b3fc-2c963f66afa6", "ON_LUNCH"
        ],
        [
            "a1a90308-d650-4897-b429-d15fcc500ff6", "Chandler Bing", "2024-01-02 22:54:41.177 +02", "CALL_STARTED", "37a55868-d979-4f41-a60d-bbba7df98790", "OFF"
        ],
        [
            "454d383a-5e89-4551-b7cb-4a755abee2be", "Ross Galler", "2024-01-03 12:54:41.177 +02", "CALL_STARTED", "8980590c-f58a-40fd-99b7-581c11b909a9", "ON_DUTY"
        ],
        [
            "67055255-bad5-4b23-bb4b-2752abf232c0", "Monika Galler", "2024-01-03 07:54:41.177 +02", "CALL_STARTED", "c6a4915f-ceb2-4bc1-bc32-04781e879fbd", "BUSY"
        ],
        [
            "c6a4915f-ceb2-4bc1-bc32-04781e879fbd", "Fibi Buffe", "2024-01-01 12:54:41.177 +02", "CALL_STARTED", "f90388a5-2b2c-4247-ba27-bbab60d35a39", "ON_CALL"
        ]
      ],
      rows: [],
      columns: [
        'AgentId',
        'AgentName',
        'TimestampUtc',
        'Action',
        'QueueIds',
        'AgentState'
      ],
      sortIndex: null,
      sortDirection: null
    }
  },
  methods: {
    sortRecords (index) {
      if (this.sortIndex === index) {
        switch (this.sortDirection) {
          case null:
            this.sortDirection = 'asc';
            break;
          case 'asc':
            this.sortDirection = 'desc';
            break;
          case 'desc':
            this.sortDirection = null;
            break;
        }
      } else {
        this.sortDirection = 'asc'
      }

      this.sortIndex = index;

      this.rows = this.rows.sort(
        (rowA, rowB) => {
          if (this.sortDirection === 'desc') {
            return rowB[index].localeCompare(rowA[index]);
          }

          return rowA[index].localeCompare(rowB[index]);
        }
      )
    },
  },
  mounted () {
    this.rows = [...this.rawRows];
  }
}
</script>