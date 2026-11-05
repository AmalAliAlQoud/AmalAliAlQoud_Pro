import 'dart:convert';
import 'package:http/http.dart' as http;
import 'ConectionString.dart';


class PatientsServices {
  Future<List<dynamic>> getPatients() async {
    try {
      final response = await http.get(
        Uri.parse(ConectionString.patients),
      );

      print('Response status: ${response.statusCode}');
      print('Response body: ${response.body}');

      if (response.statusCode == 200) {
        final data = jsonDecode(response.body);

        if (data is Map && data.containsKey('patients')) {
          return data['patients'] ?? [];
        }

        if (data is List) {
          return data;
        }
      }
    } catch (e) {
      print('Error fetching patients: $e');
    }
    return [];
  }
}